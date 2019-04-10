// LuaTest.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include <iostream>
#include <lua.hpp>

void test1()
{
	lua_State *l = luaL_newstate();
	luaL_openlibs(l);
	luaL_dofile(l, "Main.lua");
	lua_close(l);
	
}


void test2()
{
	lua_State *L = luaL_newstate();

	lua_pushstring(L, "hello world");
	
	lua_pushnumber(L, 10);

	if (lua_isnumber(L, -1) && lua_isstring(L, -2))
	{
		int num = lua_tonumber(L, -1);
		const char* str = lua_tostring(L, -2);
		
		//出栈2个元素
		lua_pop(L, 2);

		printf("%d %s\n", num, str);

		lua_close(L);
	}

}

void viewStack(lua_State* L)
{
	int i;
	int top = lua_gettop(L);
	for (int i = 1; i <= top; i++)
	{
		int t = lua_type(L, i);
		switch (t) {
		case LUA_TSTRING:
		{
			// string 
			printf("%s", lua_tostring(L, i));
			break;
		}
		case LUA_TBOOLEAN:
		{
			//  _Bool 
			printf(lua_toboolean(L, i) ? "true" : "false");
			break;
		}
		case LUA_TNUMBER:
		{
			printf("%g", lua_tonumber(L, i));
			break;
		}
		default:
		{
			printf("%s", lua_typename(L, t));
			break;
		}
		}
		printf("     ");
	}
	printf("\n");
}

const char* getStringField(lua_State* L, const char* key)
{
	const char* name;
	lua_pushstring(L, key);
	viewStack(L);
	lua_gettable(L, -2);
	if (!lua_isstring(L, -1)) {
		printf("the key is not string");
	}
	name = lua_tostring(L, -1);
	lua_pop(L, 1);
	return name;
}

int width = 100;

void test3()
{
	
	lua_State *L = luaL_newstate();
	luaL_openlibs(L);

	int error = luaL_loadfile(L, "test.lua") || lua_pcall(L, 0, 0, 0);
	if (error)
	{
		fprintf(stderr, "%s", lua_tostring(L, -1));
		lua_pop(L, -1);
	}

	lua_getglobal(L, "width");
	lua_getglobal(L, "height");
	lua_getglobal(L, "people");

	int w = lua_tointeger(L, -3);
	int h = lua_tointeger(L, -2);

	 

	lua_getfield(L, -1, "name");
	const char*  ssss = lua_tostring(L, -1);

	//viewStack(L);

	//int age = lua_tonumber(L, -1);
	//
	//lua_pop(L, 1);

	//const char* name = getStringField(L, "name");

	//printf("w=%d, h=%d", w, h);
	//printf("age is %d,name is %s :", age, name);

}

//800 600 table 13
//800 600 table name
//w = 800, h = 600 age is 13, name is YGH

 

int main()
{
 
	test3();
	
	system("pause");


	return 0;
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门提示: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
