using Fergeda_2023.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Compras
{
    public partial class VistaRequ : Form
    {
        int folio;
        private Consultas X;
        private formatoTablas Tb;
        private estiloVentana est;
        public VistaRequ(int folio)
        {
            X = new Consultas();
            Tb = new formatoTablas();
            est = new estiloVentana();
            this.folio = folio;
            InitializeComponent();
            est.estilo(this);
            carga();
            Tb.colores(dataGridView1, 1);
        }
        private void carga()
        {
            X.tablaDatos(dataGridView1, "SELECT fk_oc 'Orden de Compra', descripcion 'Descripción', no_pza 'Cantidad', unidad 'Unidad', parte_producto " +
                "'No. parte',  Marca 'Marca', Obgetivo 'Objetivo', idc_requsiciondesgloce 'ID' FROM  fergeda.c_requsiciondesgloce where fk_requi = '" + folio + "'; ");
        }
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int Columna = dataGridView1.CurrentCell.ColumnIndex;
                switch (Columna)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                        {
                            if (X.elem3("SELECT if(fk_oc is null,'0',fk_oc) FROM  fergeda.c_requsiciondesgloce where idc_requsiciondesgloce='" + dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';").Equals("0"))
                            {
                                X.insertar("UPDATE fergeda.c_requsiciondesgloce SET fk_oc='" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' WHERE idc_requsiciondesgloce='" + dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");

                                if (X.elem3("SELECT count(*) FROM fergeda.c_requsiciondesgloce where fk_requi='" + folio + "' and fk_oc is null;").Equals("0"))
                                {
                                    X.insertar("update fergeda.c_requisicion set status='Terminado',fecha_cierre=now() where idc_requisicion='" + folio + "';");
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}
