using SQLite;
using xleguisamoS5.Models;

namespace xleguisamoS5.Repositories
{
    public class PersonRepository
    {
        private readonly string _dbPath;
        private SQLiteConnection? _conn;

        public string StatusMessage { get; set; } = string.Empty;

        public PersonRepository(string path)
        {
            _dbPath = path;
        }

        private void Init()
        {
            if (_conn is not null)
                return;

            _conn = new SQLiteConnection(_dbPath);
            _conn.CreateTable<Persona>();
        }

        public void AddNewPerson(string? name)
        {
            try
            {
                Init();

                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("El nombre es requerido");

                var person = new Persona { Name = name.Trim() };
                int result = _conn!.Insert(person);
                StatusMessage = $"Dato ingresado ({result} registro): {person.Name}";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return _conn!.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }

            return new List<Persona>();
        }

        public bool DeletePerson(int id)
        {
            try
            {
                Init();

                int result = _conn!.Delete<Persona>(id);
                if (result == 0)
                {
                    StatusMessage = $"No se encontró la persona con Id {id}";
                    return false;
                }

                StatusMessage = "Persona eliminada correctamente";
                return true;
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
                return false;
            }
        }
    }
}
