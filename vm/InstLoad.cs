using LuaVM = luavm.state.LuaState;

namespace luavm.vm
{
    public static class InstLoad
    {
        //����ļĴ���ֻ��index��ʾ������ax��bx�����й̶����ֵ�
        internal static void LoadNil(Instruction i, ref LuaVM vm)
        {
            var (a, b, _) = i.ABC();
            a += 1;//��ʼ�Ĵ�������

            vm.PushNil();//��nil Push���Ĵ�����
            for (var j = a; j <= a + b; j++)
            {
                vm.Copy(-1, j);//�ѵ�һ��push��nil���������漸��������
            }

            vm.Pop(1);//push��һ��������nil
        }

        internal static void LoadBool(Instruction i, ref LuaVM vm)
        {
            var (a, b, c) = i.ABC();
            a += 1;//��ʼ�Ĵ�������

            vm.PushBoolean(b != 0);//pushĿ�겼��ֵ
            vm.Replace(a);//pop��ջ��ֵ��Ȼ��ֵ��Ŀ�������ļĴ���
            if (c != 0)//���C���㣬��������һ��ָ��
            {
                vm.AddPC(1);
            }
        }

        internal static void LoadK(Instruction i, ref LuaVM vm)
        {
            var (a, bx) = i.ABx();
            a += 1;

            vm.GetConst(bx);//bx�ǳ����������������ǰ�Ŀ��ֵpush���Ĵ�����
            vm.Replace(a);//�ڿ���
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