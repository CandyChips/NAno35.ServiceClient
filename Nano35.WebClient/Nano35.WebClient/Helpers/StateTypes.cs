namespace Nano35.WebClient.Helpers
{
    public enum RepairOrderStateType
    {
        Created = 0,
        PerformerChanged = 1,
        Diagnosis = 2,
        Agreement = 3,
        OnWait = 4,
        OnWork = 5,
        Ready = 6,
        Completed = 7,
        Closed = 8,
        None = 99
    }

    public class RepairOrderStateTypesHelper
    {
        public static int Created => 0;
        public static int PerformerChanged => 1;
        public static int Diagnosis => 2;
        public static int Agreement => 3;
        public static int OnWait => 4;
        public static int OnWork => 5;
        public static int Ready => 6;
        public static int Completed => 7;
        public static int Closed => 8;
        public static int None => 99;
        
        public static string GetName(int id)
        {
            return 
                id == Created ? "Заказ создан" :
                id == PerformerChanged ? "Изменен исполнитель" :
                id == Diagnosis ? "Заказ на диагностике" :
                id == Agreement ? "Заказ выполнен" :
                id == OnWait ? "Заказ ожидает запчасти" :
                id == OnWork ? "Заказ в работе" :
                id == Ready ? "Заказ готов" :
                id == Completed ? "Заказ готов к выдаче" : 
                id == Closed ? "Заказ  закрыт" :
                "Статус";
        }
    }
}