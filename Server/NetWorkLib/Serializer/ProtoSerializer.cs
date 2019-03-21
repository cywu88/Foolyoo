﻿using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NetWorkLib
{
    public class ProtoSerializer : ISerializer
    {
        private Dictionary<string, int> m_protocolNum = new Dictionary<string, int>();
        private Dictionary<int, string> m_numProtocal = new Dictionary<int, string>();

     

        private Assembly assembly
        {
            get
            {
                var _asmb = Assembly.GetEntryAssembly();
                if (_asmb == null)
                    _asmb = Assembly.GetExecutingAssembly();
                if (_asmb == null)
                    _asmb = Assembly.GetCallingAssembly();
                return _asmb;
            }
        }


        public void Init()
        {
            LoadProtoNum();
        }

        private void LoadProtoNum()
        {
            var protoNumEnumType = assembly.GetType("Message.ProtoNum");
            m_protocolNum.Clear();
            m_numProtocal.Clear();

            foreach (var value in Enum.GetValues(protoNumEnumType))
            {
                int protoNum = (int)value;
                string protoName = Enum.GetName(protoNumEnumType, protoNum);
                var attr = (Google.Protobuf.Reflection.OriginalNameAttribute)(protoNumEnumType.GetField(protoName)
                    .GetCustomAttributes(typeof(Google.Protobuf.Reflection.OriginalNameAttribute), false).FirstOrDefault());
                protoName = attr.Name;
                protoName = protoNumEnumType.Namespace + "." + protoName;
                m_protocolNum.Add(protoName, protoNum);
                m_numProtocal.Add(protoNum, protoName);
            }
        }

        private byte[] TypeToHeader(Type t)
        {
            int num = m_protocolNum[t.ToString()];
            return BitConverter.GetBytes((ushort)num);
        }

        private void AddHeader(ref byte[] data, Type type)
        {
            byte[] _data = new byte[data.Length + 2];
            byte[] header = TypeToHeader(type);
            Array.Copy(header, 0, _data, 0, 2);
            Array.Copy(data, 0, _data, 2, data.Length);
            data = _data;
        }

        // 将消息序列化为二进制的方法
        // < param name="model">要序列化的对象< /param>
        private byte[] _Serialize(IMessage model)
        {
            try
            {
                //涉及格式转换，需要用到流，将二进制序列化到流中
                using (MemoryStream ms = new MemoryStream())
                {
                    model.WriteTo(ms);
                    //定义二级制数组，保存序列化后的结果
                    byte[] result = new byte[ms.Length];
                    //将流的位置设为0，起始点
                    ms.Position = 0;
                    //将流中的内容读取到二进制数组中
                    ms.Read(result, 0, result.Length);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("序列化失败: " + ex.ToString());
                return null;
            }

        }

        public byte[] Serialize(object proto)
        {
            byte[] data = _Serialize(proto as IMessage);
            AddHeader(ref data, proto.GetType());
            return data;
        }

        public object Deserialize(byte[] data)
        {
            if (data.Length < 2)
            {
                Logger.LogError("receive data length: " + data.Length);
                return null;
            }

            byte[] header = new byte[2];
            Array.Copy(data, header, 2);

            byte[] body = new byte[data.Length - 2];
            Array.Copy(data, 2, body, 0, data.Length - 2);
            Type type = HeaderToType(header);

            if (type == null)
            {
                Logger.LogError("Wrong Type header : [" + header[0] + "," + header[1] + "]");
                return null;
            }
            try
            {
                return _Deserialize(body, type); ;
            }
            catch (Exception e)
            {
                Logger.LogError(type + " : DeSerialize Failed \n" + e);
            }
            return null;
        }

        private Type GetType(string name)
        {
            return assembly.GetType(name);
        }


        private Type HeaderToType(byte[] header)
        {
            int num = BitConverter.ToUInt16(header, 0);
            if (!m_numProtocal.ContainsKey(num))
            {
                Logger.LogError("not found protonum: " + num);
                return null;
            }

            Type type = GetType(m_numProtocal[num]);
            if (type == null)
                Logger.LogError("null message num: " + num);
            return type;
        }

        // 将收到的消息反序列化成对象
        // < returns>The serialize.< /returns>
        // < param name="msg">收到的消息.</param>
        private IMessage _Deserialize(byte[] msg, Type type)
        {
            try
            {
                BindingFlags flag = BindingFlags.Static | BindingFlags.NonPublic;
                FieldInfo field = type.GetField("_parser", flag);
                object parser = field.GetValue(null);
                var method = parser.GetType().GetMethod("ParseFrom", new Type[] { typeof(byte[]) });
                var result = method.Invoke(parser, new object[] { msg });
                return result as IMessage;
            }
            catch (Exception ex)
            {
                Logger.LogError("反序列化失败: " + ex.ToString());
                return null;
            }
        }
    }
}
