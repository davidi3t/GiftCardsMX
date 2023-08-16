using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using System.Text;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;
using OpenPop.Pop3.Exceptions;
using OpenPop.Common.Logging;
using Message = OpenPop.Mime.Message;

using System.Data.SqlClient;

public partial class Verificacion : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnValidar_Click(object sender, EventArgs e)
    {

        Style notFoundStyle = new Style();
        notFoundStyle.ForeColor = System.Drawing.Color.Red;
        Style foundStyle = new Style();
        foundStyle.ForeColor = System.Drawing.Color.Green;

        lblDisponible.Text = string.Empty;
        lblImporte.ApplyStyle(foundStyle);

        lblImporte.Text = string.Empty;
        lblImporte.ApplyStyle(foundStyle);

        if (!String.IsNullOrEmpty(txtNoAutorizacion.Text) && !txtNoAutorizacion.Text.Contains("d") && !txtNoAutorizacion.Text.Contains("D") && !txtNoAutorizacion.Text.Contains("r") && !txtNoAutorizacion.Text.Contains("R") && !txtNoAutorizacion.Text.Contains("b") && !txtNoAutorizacion.Text.Contains("B"))
        {

            string noAutorizacion = string.Empty; // Substring de No. de Autorizacion
            string fechaDeposito = string.Empty;
            DateTime dtFechaDeposito = new DateTime();
            string strImporte = string.Empty; // Substring del importe
            int autorizacion = 0;   //Numero de Autorizacion
            int txtAutorizacionInt = 0; // Numero de autorizacion ingresado por usuario
            string valid = "False";

            DataSet depositDataSet = null;
            int.TryParse(noAutorizacion, out autorizacion);
            int.TryParse(txtNoAutorizacion.Text, out txtAutorizacionInt);


            depositDataSet = null;// SelectDeposit(txtAutorizacionInt);




            if (depositDataSet != null)
            {
                foreach (DataRow dr in depositDataSet.Tables[0].Rows)
                {
                    valid = dr["valid_bit"].ToString();
                    strImporte = dr["amount"].ToString();
                }

                depositDataSet = null;
            }


            if (valid == "True")
            {
                decimal dvalor = Convert.ToDecimal(strImporte);


                if (!string.IsNullOrEmpty(strImporte))
                    lblImporte.Text = "$" + dvalor.ToString("#.##");

                lblImporte.ApplyStyle(foundStyle);
                lblImporte.Visible = true;
                lblDisponible.ApplyStyle(foundStyle);
                lblDisponible.Visible = true;
                lblDisponible.Text = "Tienes disponible para comprar!";

            }
            else
            {
                lblDisponible.ApplyStyle(notFoundStyle);
                //lblImporte.Visible = true;
                lblDisponible.Visible = true;
                lblDisponible.Text = "Deposito no encontrado. Contactame.";


            }



            if (string.IsNullOrEmpty(strImporte) && 1==2)
            {

                Pop3Client pop3Client = new Pop3Client();
                int insertCount = 0;


                depositDataSet = SelectDeposit(0);


                try
                {

                    if (pop3Client.Connected)
                        pop3Client.Disconnect();

                    pop3Client.Connect("pop3.live.com", 995, true);
                    pop3Client.Authenticate(Constants.email, Constants.emailpass);


                    int count = pop3Client.GetMessageCount();

                    for (int i = count; i >= 1; i -= 1)
                    {


                        Message message = pop3Client.GetMessage(i);
                        string strBody = string.Empty;

                        

                        string msgFrom = message.Headers.From.ToString();

                        if (msgFrom.Contains("notificaciones@banamex.com") && message.Headers.Subject.ToString() == "Deposito a la Cuenta Banamex")
                        {


                            // Find the first text/plain version
                            MessagePart plainTextPart = message.FindFirstPlainTextVersion();

                            if (plainTextPart != null)
                            {
                                // The message had a text/plain version - show that one
                                strBody = plainTextPart.GetBodyAsText();
                            }
                            else
                            {
                                // Try to find a body to show in some of the other text versions
                                List<MessagePart> textVersions = message.FindAllTextVersions();

                                if (textVersions.Count >= 1)
                                    strBody = textVersions[0].GetBodyAsText();
                            }


                            if (!string.IsNullOrEmpty(strBody))
                            {
                                if (strBody.IndexOf("<b>No. de Autorizaci&oacute;n:</b></td>") > 1)
                                {
                                    noAutorizacion = strBody.Substring(strBody.IndexOf("<b>No. de Autorizaci&oacute;n:</b></td>"), 151);

                                    //Fecha de Deposito. 
                                     fechaDeposito = strBody.Substring((strBody.IndexOf("Fecha y hora:")+123),8);

                                    string auxDate = fechaDeposito.Substring(3,2) + "/" + fechaDeposito.Substring(0,2) + "/" + fechaDeposito.Substring(6,2);

                                    if(DateTime.TryParse(auxDate, out dtFechaDeposito))
                                         auxDate = fechaDeposito;
                                        

                                    noAutorizacion = noAutorizacion.Replace(" ", "");

                                    // if (noAutorizacion.Length == 140)
                                    noAutorizacion = noAutorizacion.Substring(noAutorizacion.IndexOf("\">") + 2, ((noAutorizacion.Length - noAutorizacion.IndexOf("\">") - 7)));
                                    //noAutorizacion = noAutorizacion.Substring(noAutorizacion.IndexOf("\">") + 2, (noAutorizacion.Length - (noAutorizacion.IndexOf("\">") + 8)));

                                    // if (noAutorizacion.Length == 139)
                                    //     noAutorizacion = noAutorizacion.Substring(noAutorizacion.IndexOf("\">") + 2, (noAutorizacion.Length - (noAutorizacion.IndexOf("\">") + 9)));

                                    //Just to make sure

                                    noAutorizacion = noAutorizacion.Replace(">", "");
                                    noAutorizacion = noAutorizacion.Replace("<", "");
                                    noAutorizacion = noAutorizacion.Replace(@"\", "");
                                    noAutorizacion = noAutorizacion.Replace("t", "");
                                    noAutorizacion = noAutorizacion.Replace("d", "");
                                    noAutorizacion = noAutorizacion.Replace(@"/", "");


                                    if (!string.IsNullOrEmpty(noAutorizacion) && !string.IsNullOrEmpty(txtNoAutorizacion.Text))
                                    {

                                        int.TryParse(noAutorizacion, out autorizacion);
                                        int.TryParse(txtNoAutorizacion.Text, out txtAutorizacionInt);


                                        if (autorizacion == txtAutorizacionInt)
                                        {
                                            strImporte = strBody.Substring(strBody.IndexOf(">$") + 1, (strBody.IndexOf("M.N. <") - (strBody.IndexOf(">$") + 1)));

                                            if (!string.IsNullOrEmpty(strImporte))
                                                lblImporte.Text = strImporte;

                                            lblImporte.ApplyStyle(foundStyle);
                                            lblImporte.Visible = true;
                                            lblDisponible.ApplyStyle(foundStyle);
                                            lblDisponible.Visible = true;
                                            lblDisponible.Text = "Tienes disponible para comprar!";


                                            lblImporte.Text = lblImporte.Text.Replace("$", "");



                                            //Insertar deposito en DB

                                            try
                                            {
                                                SqlParameter[] arParams = new SqlParameter[5];

                                                arParams[0] = new SqlParameter("@autorization", SqlDbType.Int);
                                                arParams[0].Value = autorizacion;

                                                arParams[1] = new SqlParameter("@amount", SqlDbType.Money);
                                                if (!string.IsNullOrEmpty(lblImporte.Text))
                                                    arParams[1].Value = Convert.ToDecimal(lblImporte.Text);
                                                else
                                                    arParams[1].Value = 0;

                                                arParams[2] = new SqlParameter("@deposit_date", SqlDbType.DateTime);
                                                arParams[2].Value = dtFechaDeposito.Date;

                                                arParams[3] = new SqlParameter("@valid_bit", SqlDbType.Bit);
                                                arParams[3].Value = 1;

                                                arParams[4] = new SqlParameter("@user", SqlDbType.VarChar);
                                                arParams[4].Value = "giftcardsmx.com";

                                                SqlHelper helpersql = new SqlHelper();

                                                helpersql.ExecuteNonQuery(Constants.DataBaseName, "saveDeposit", CommandType.StoredProcedure, arParams);
                                            }
                                            catch
                                            {
                                                throw;
                                            }


                                            break; // Encontrado y saliendo sin checar mas

                                        }
                                        else // Deposito NO se ha Encontrado
                                        {

                                            

                                            if (insertCount <= 20)
                                            {

                                                //Fecha de Deposito. 
                                                fechaDeposito = strBody.Substring((strBody.IndexOf("Fecha y hora:") + 123), 8);

                                                string auxDate2 = fechaDeposito.Substring(3, 2) + "/" + fechaDeposito.Substring(0, 2) + "/" + fechaDeposito.Substring(6, 2);

                                                if (DateTime.TryParse(auxDate2, out dtFechaDeposito))
                                                    auxDate2 = fechaDeposito;

                                                strImporte = strBody.Substring(strBody.IndexOf(">$") + 1, (strBody.IndexOf("M.N. <") - (strBody.IndexOf(">$") + 1)));

                                                strImporte = strImporte.Replace("$", "");

                                                bool depositoExiste = false;

                                                if (depositDataSet != null)
                                                {
                                                    foreach (DataRow dr in depositDataSet.Tables[0].Rows)
                                                    {
                                                        if (noAutorizacion == dr["autorization"].ToString())
                                                        {
                                                            depositoExiste = true;

                                                            break;
                                                        }
                                                        
                                                    }

                                                   
                                                }


                                                //Insertar deposito en DB
                                                if (!depositoExiste)
                                                {
                                                    try
                                                    {
                                                        SqlParameter[] arParams = new SqlParameter[5];

                                                        arParams[0] = new SqlParameter("@autorization", SqlDbType.Int);
                                                        arParams[0].Value = autorizacion;

                                                        arParams[1] = new SqlParameter("@amount", SqlDbType.Money);
                                                        if (!string.IsNullOrEmpty(strImporte))
                                                            arParams[1].Value = Convert.ToDecimal(strImporte);
                                                        else
                                                            arParams[1].Value = 0;

                                                        arParams[2] = new SqlParameter("@deposit_date", SqlDbType.DateTime);
                                                        arParams[2].Value = dtFechaDeposito.Date;

                                                        arParams[3] = new SqlParameter("@valid_bit", SqlDbType.Bit);
                                                        arParams[3].Value = 1;

                                                        arParams[4] = new SqlParameter("@user", SqlDbType.VarChar);
                                                        arParams[4].Value = "giftcardsmx.com";

                                                        SqlHelper helpersql = new SqlHelper();

                                                        helpersql.ExecuteNonQuery(Constants.DataBaseName, "saveDeposit", CommandType.StoredProcedure, arParams);

                                                        insertCount++;

                                                    }
                                                    catch
                                                    {
                                                        throw;
                                                    }
                                                }


                                            }





                                            lblDisponible.ApplyStyle(notFoundStyle);
                                            //lblImporte.Visible = true;
                                            lblDisponible.Visible = true;
                                            lblDisponible.Text = "Deposito No Encontrado";
                                        }
                                    }

                                }

                            }
                        }
                        else // No hay mensajes de deposito
                        {


                            lblDisponible.ApplyStyle(notFoundStyle);
                            lblDisponible.Visible = true;
                            lblDisponible.Text = "Deposito No Encontrado";

                        }
                    }
                }
                catch //(Exception ex1)
                {
                    lblDisponible.ApplyStyle(notFoundStyle);
                    lblDisponible.Visible = true;
                    lblDisponible.Text = "Procesando depositos... intenta mas tarde.";

                    /*
                    if (ex1 != null && ex1.InnerException != null && ex1.InnerException.ToString().Contains(" Exceeded"))
                    {
                        lblDisponible.ApplyStyle(notFoundStyle);
                        lblDisponible.Visible = true;
                        lblDisponible.Text = "Estamos procesando, por favor intenta mas tarde.";

                        /*
                         Para evitar esto pudieramos traernos los ultimos depositos recibidos cada vez que alguien haga un request.
                         e insertarlos en alguna tabla para tenerlos ya disponibles. 
                         Podemos checar en primer lugar esa tabla y si no mandar llamar al correo. 
                         */
                    //throw;


                }
            }

        }
        else
        {

            if (!String.IsNullOrEmpty(txtNoAutorizacion.Text) && (txtNoAutorizacion.Text.Contains("d") || txtNoAutorizacion.Text.Contains("D") || txtNoAutorizacion.Text.Contains("r") || txtNoAutorizacion.Text.Contains("R") || txtNoAutorizacion.Text.Contains("b") || txtNoAutorizacion.Text.Contains("B")))
            {

                string autorizacion = txtNoAutorizacion.Text;

                autorizacion = autorizacion.Replace("d", "");
                autorizacion = autorizacion.Replace("D", "");
                autorizacion = autorizacion.Replace("r", "");
                autorizacion = autorizacion.Replace("R", "");
                autorizacion = autorizacion.Replace("B", "");
                autorizacion = autorizacion.Replace("b", "");


                try
                {
                    SqlParameter[] arParams = new SqlParameter[5];

                    arParams[0] = new SqlParameter("@autorization", SqlDbType.Int);
                    arParams[0].Value = autorizacion;

                    arParams[1] = new SqlParameter("@amount", SqlDbType.Money);
                    if (!string.IsNullOrEmpty(lblImporte.Text))
                        arParams[1].Value = 0;

                    arParams[2] = new SqlParameter("@deposit_date", SqlDbType.DateTime);
                    arParams[2].Value = DateTime.Now;

                    arParams[3] = new SqlParameter("@valid_bit", SqlDbType.Bit);
                    arParams[3].Value = 0;

                    arParams[4] = new SqlParameter("@user", SqlDbType.VarChar);
                    arParams[4].Value = "becerrd1";

                    SqlHelper helpersql = new SqlHelper();

                    helpersql.ExecuteNonQuery(Constants.DataBaseName, "saveDeposit", CommandType.StoredProcedure, arParams);


                    lblDisponible.ApplyStyle(notFoundStyle);
                    lblDisponible.Visible = true;
                    lblDisponible.Text = "Deposito invalidado exitosamente.";

                }
                catch
                {
                    throw;
                }

            }
            else
            {

                lblDisponible.ApplyStyle(notFoundStyle);
                lblDisponible.Visible = true;
                lblDisponible.Text = "Por favor ingresa el No. de Autorizacion de tu recibo.";
            }
        }
    }


    private DataSet SelectDeposit(int autorization)
    {

        DataSet dataSet = null;

        try
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@autorization", SqlDbType.Int);
            sqlParams[0].Value = autorization;



            dataSet = SqlHelper.ExecuteDataSet(Constants.DataBaseName, "selectDeposit", sqlParams);
        }
        catch (Exception ex)
        {
            throw;
        }

        return dataSet;
    }


    protected void txtNoAutorizacion_TextChanged(object sender, EventArgs e)
    {

    }
}