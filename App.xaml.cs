using xleguisamoS5.Repositories;

namespace xleguisamoS5
{
    public partial class App : Application
    {
        public static PersonRepository _personRepo { get; set; } = null!;

        public App(PersonRepository personRepository)
        {
            InitializeComponent();
            _personRepo = personRepository;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.vPersona());
        }
    }
}
