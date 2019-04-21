using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityCRUD
{
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
        }

        private void CargarGrid()
        {
            using (AdventureEntities context = new AdventureEntities())
            {
                /*
                    IQueryable: Proporciona funcionalidad para evaluar consultas con
                    respecto a un origen de datos concreto en el que se especifica el tipo de los datos.
                    Util para aplicar consultas LINQ
                 */
                IQueryable<Customer> cliente = from q in context.Customer
                                               select q;

                List<Customer> lista = cliente.ToList();

                dgvCustomer.DataSource = lista;
                dgvCustomer.Refresh();
            }
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCustomerId.Text);
            using (AdventureEntities context = new AdventureEntities()) {
                IQueryable<Customer> cliente = from q in context.Customer
                                               where q.CustomerID == id
                                               select q;
                List<Customer> lista = cliente.ToList();

                var oCliente = lista[0];

                ckbNameStyle.Checked = oCliente.NameStyle;
                txtTitle.Text = oCliente.Title;
                txtFirstName.Text = oCliente.FirstName;
                txtMiddleName.Text = oCliente.MiddleName;
                txtLastName.Text = oCliente.LastName;
                txtSuflix.Text = oCliente.Suffix;
                txtCompany.Text = oCliente.CompanyName;
                txtSalePerson.Text = oCliente.SalesPerson;
                txtEmail.Text = oCliente.EmailAddress;
                txtPhone.Text = oCliente.Phone;
                txtPassHash.Text = oCliente.PasswordHash;
                txtPassSalt.Text = oCliente.PasswordSalt;
            }
        }

        private void txtCustomerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return) {
                btnSearch_Click(sender, e);
            }
        }
    }
}
