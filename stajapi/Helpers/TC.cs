namespace stajapi.Helpers
{
    public static class TC
    {
        public static string GenerateRandomTC()
        {
            Random rnd = new Random();
            string tc = "";
            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                    tc += rnd.Next(1, 10).ToString();
                else
                    tc += rnd.Next(0, 10).ToString();
            }
            int toplam = 0;
            for (int i = 0; i < 10; i++)
            {
                toplam += Int32.Parse(tc[i].ToString());
            }
            toplam = toplam % 11;
            toplam = toplam % 10;
            tc += toplam.ToString();
            return tc;
        }
    }
}
