using System;

namespace Nano35.WebClient
{
    public static class Roles
    {
        public static Guid Storage => Guid.Parse("0474cdca-29f0-43f4-b365-3accddfc1a27");
        public static Guid Manager => Guid.Parse("45050ae0-06c8-4d5c-ae5c-4e6831504ca3");
        public static Guid Cashbox => Guid.Parse("35075b7c-74b6-415d-9d16-5fb089c5b4be");
        public static Guid Master => Guid.Parse("bdee02b1-1b7a-4aab-a269-7a9235287af9");
        public static Guid Admin => Guid.Parse("58688ab3-a30c-4385-9310-f20fea1d4082");

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