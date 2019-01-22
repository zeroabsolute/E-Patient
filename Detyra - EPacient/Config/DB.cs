namespace Detyra___EPacient.Config {
    class DB {
        private static string SERVER_URL = "127.0.0.1";
        private static string DB_USERNAME = "root";
        private static string DB_PASSWORD = "";
        private static string DB_NAME = "e_pacient";
        private static int DB_PORT = 3306;

        public static string connectionString = $@"
            Server={SERVER_URL};
            Port={DB_PORT};
            Database={DB_NAME};
            Uid={DB_USERNAME};
            Password={DB_PASSWORD};
            convert zero datetime=True";
    }
}
