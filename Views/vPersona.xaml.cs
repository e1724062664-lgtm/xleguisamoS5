using xleguisamoS5.Models;

namespace xleguisamoS5.Views;

public partial class vPersona : ContentPage
{
    private int? _idEditando = null;
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

        if (_idEditando is int id)
        {
            bool ok = App._personRepo.UpdatePerson(id, txtNombre.Text);
            lblMenssage.Text = App._personRepo.StatusMessage;
            if (!ok) return;          // conserva el texto si hubo error

            _idEditando = null;
            btnAgregar.Text = "Agregar persona";
        }
        else
        {
            App._personRepo.AddNewPerson(txtNombre.Text);
            lblMenssage.Text = App._personRepo.StatusMessage;
        }

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

        if (_idEditando == id)
        {
            _idEditando = null;
            txtNombre.Text = "";
            btnAgregar.Text = "Agregar persona";
        }
        lblMenssage.Text = App._personRepo.StatusMessage;
        CargarLista();
    }

    private void btnEditar_Clicked(object? sender, EventArgs e)
    {
        if (sender is not Button { CommandParameter: Persona persona })
            return;

        _idEditando = persona.Id;
        txtNombre.Text = persona.Name;
        btnAgregar.Text = "Guardar cambios";
        lblMenssage.Text = $"Editando Id {persona.Id}";
        txtNombre.Focus();
    }
}
