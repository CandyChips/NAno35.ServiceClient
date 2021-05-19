namespace Nano35.WebClient.Helpers
{
    public enum RepairOrderStateType
    {
        Created = 0,
        InWork = 1,
        Completed = 2,
        Stopped = 3,
        OnWait = 4,
        PerformerChanged = 5,
        Closed = 6
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