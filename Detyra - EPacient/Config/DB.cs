namespace Detyra___EPacient.Config {
    class DB {
        private static string SERVER_URL = "localhost";
        private static string DB_USERNAME = "root";
        private static string DB_NAME = "e_pacient";
        private static int DB_PORT = 8204;

        public static string connectionString = $"server={SERVER_URL};user={DB_USERNAME};database={DB_NAME};port={DB_PORT}";
    }
}
