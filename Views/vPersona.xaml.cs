namespace xleguisamoS5.Views;

public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();
    }

    private void CargarLista()
    {
        listaPersonas.ItemsSource = App._personRepo.GetAllPerson();
    }

    private void btnAgregar_Clicked(object? sender, EventArgs e)
    {
        lblMenssage.Text = "";
        App._personRepo.AddNewPerson(txtNombre.Text);
        lblMenssage.Text = App._personRepo.StatusMessage;
        txtNombre.Text = "";
        CargarLista();
    }

    private void btnMostrar_Clicked(object? sender, EventArgs e)
    {
        CargarLista();
    }

    private async void btnEliminar_Clicked(object? sender, EventArgs e)
    {
        if (sender is not Button { CommandParameter: int id })
            return;

        bool confirmar = await DisplayAlertAsync(
            "Eliminar", "¿Desea eliminar esta persona?", "Sí", "No");
        if (!confirmar)
            return;

        App._personRepo.DeletePerson(id);
        lblMenssage.Text = App._personRepo.StatusMessage;
        CargarLista();
    }
}
