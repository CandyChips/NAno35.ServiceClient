using System;

namespace Nano35.WebClient.Helpers
{
    public class ClientStateTypesHelper
    {
        public static Guid Person => Guid.Parse("9f76e798-aab2-44cf-eb9b-08d90bcf6667");
        public static Guid Organisation => Guid.Parse("0a0e079d-dd41-4009-eb9c-08d90bcf6667");
        
        public static string GetName(Guid id)
        {
            return 
                id == Person ? "Физическое лицо" :
                id == Organisation ? "Юридическое лицо" :
                "";
        }
    }
    	
    	
}