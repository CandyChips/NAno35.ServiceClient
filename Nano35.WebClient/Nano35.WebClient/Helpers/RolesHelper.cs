using System;

namespace Nano35.WebClient.Helpers
{
    public class RolesHelper
    {
        public static Guid Storage => Guid.Parse("66f8433d-904d-44bb-907a-b24a68ca90c2");
        public static Guid Manager => Guid.Parse("ac914c57-fb98-41ff-8819-9d7108bda148");
        public static Guid Cashbox => Guid.Parse("1c2cb188-c1c6-4836-86a3-b2425fcaf6ca");
        public static Guid Master  => Guid.Parse("aac06f8a-6170-4de8-b18c-8f21c9b72bee");
        public static Guid Admin => Guid.Parse("ca47010e-155d-4344-a190-9915eb82bf36");

        public static string GetName(Guid id)
        {
            return 
                id == Storage ? "Кладовщик" :
                id == Manager ? "Менеджер" :
                id == Cashbox ? "Бухгалтер" :
                id == Master ? "Мастер" : 
                "Администратор";
        }
    }
}