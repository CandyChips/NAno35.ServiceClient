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
        public static int InWork => 1;
        public static int Completed => 2;
        public static int Stopped => 3;
        public static int OnWait => 4;
        public static int PerformerChanged => 5;
        public static int Closed => 6;
        
        public static string GetName(int id)
        {
            return 
                id == Created ? "Заказ на ремонт создан" :
                id == InWork ? "Заказ на ремонт в работе" :
                id == Completed ? "Заказ на ремонт выполнен" :
                id == OnWait ? "Заказ на ремонт ожидает запчасти" :
                id == PerformerChanged ? "Изменен исполнитель" :
                id == Closed ? "Заказ на ремонт закрыт" :
                id == Stopped ? "Заказ на ремонт приостановлен" : 
                "";
        }
    }
}