using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtCustomerId.Text);

                if (id <= 0)
                {
                    //Inserta
                    Insertar();
                }
                else
                {
                    //Actualiza
                    Actualizar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                CargarGrid();
            }
        }

        private void Actualizar() {
            int id = Convert.ToInt32(txtCustomerId.Text);

            using (AdventureEntities context = new AdventureEntities()) {
                Customer oCliente = context.Customer.SingleOrDefault(p => p.CustomerID == id);

                //MessageBox.Show(oCliente.LastName);

                oCliente.NameStyle = ckbNameStyle.Checked;
                oCliente.Title = txtTitle.Text;
                oCliente.FirstName = txtFirstName.Text;
                oCliente.MiddleName = txtMiddleName.Text;
                oCliente.LastName = txtLastName.Text;
                oCliente.Suffix = txtSuflix.Text;
                oCliente.CompanyName = txtCompany.Text;
                oCliente.SalesPerson = txtSalePerson.Text;
                oCliente.EmailAddress = txtEmail.Text;
                oCliente.Phone = txtPhone.Text;
                oCliente.PasswordHash = txtPassHash.Text;
                oCliente.PasswordSalt = txtPassSalt.Text;

                context.SaveChanges();
            }
        }

        private void Insertar() {
            try
            {
                using (AdventureEntities context = new AdventureEntities()) {
                    Customer oCliente = new Customer
                    {
                        NameStyle = ckbNameStyle.Checked,
                        Title = txtTitle.Text,
                        FirstName = txtFirstName.Text,
                        MiddleName = txtMiddleName.Text,
                        LastName = txtLastName.Text,
                        Suffix = txtSuflix.Text,
                        CompanyName = txtCompany.Text,
                        SalesPerson = txtSalePerson.Text,
                        EmailAddress = txtEmail.Text,
                        Phone = txtPhone.Text,
                        PasswordHash = txtPassHash.Text,
                        PasswordSalt = txtPassSalt.Text,
                        ModifiedDate = DateTime.Now
                    };

                    context.Customer.Add(oCliente);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex) {
                foreach (var validationErrors in ex.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show(string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                CargarGrid();
            }
        }
    }
}
