using LuaVM = luavm.state.LuaState;

namespace luavm.vm
{
    public static class InstLoad
    {
        //这里的寄存器只用index表示，不是ax，bx这样有固定名字的
        internal static void LoadNil(Instruction i, ref LuaVM vm)
        {
            var (a, b, _) = i.ABC();
            a += 1;//起始寄存器索引

            vm.PushNil();//把nil Push到寄存器上
            for (var j = a; j <= a + b; j++)
            {
                vm.Copy(-1, j);//把第一个push的nil拷贝到上面几个索引上
            }

            vm.Pop(1);//push第一个索引的nil
        }

        internal static void LoadBool(Instruction i, ref LuaVM vm)
        {
            var (a, b, c) = i.ABC();
            a += 1;//起始寄存器索引

            vm.PushBoolean(b != 0);//push目标布尔值
            vm.Replace(a);//pop掉栈首值，然后赋值给目标索引的寄存器
            if (c != 0)//如果C非零，则跳过下一条指令
            {
                vm.AddPC(1);
            }
        }

        internal static void LoadK(Instruction i, ref LuaVM vm)
        {
            var (a, bx) = i.ABx();
            a += 1;

            vm.GetConst(bx);//bx是常量表索引，这里是把目标值push到寄存器上
            vm.Replace(a);//在拷贝
        }

        internal static void LoadKx(Instruction i, ref LuaVM vm)
        {
            var (a, _) = i.ABx();
            a += 1;
            var ax = new Instruction(vm.Fetch()).Ax();

            vm.GetConst(ax);
            vm.Replace(a);
        }
    }
}