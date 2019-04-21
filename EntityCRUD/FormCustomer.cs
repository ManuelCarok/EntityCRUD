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
    }
}
