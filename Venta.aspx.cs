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
using System.Net.Mail;
using System.Data.SqlClient;
using GiftCardMXSpace;
public partial class Venta : Page
{

    protected double Total = 0;

    public string productImage = string.Empty;

    public string productName = string.Empty;



    protected void Page_Load(object sender, EventArgs e)
    {

            Total = 0;

            Style foundStyle = new Style();
            foundStyle.ForeColor = System.Drawing.Color.Green;
            lblDisponible.ApplyStyle(foundStyle);
            lblDisponible.Text = string.Empty;
            lblTotal.Text = string.Empty;


            string productCode = Request.QueryString["ID"];

            if (!string.IsNullOrEmpty(productCode))
            {

                Product product = new Product();

                product = product.GetProductByCode(productCode);

                lblTotal.Text = product.ProductPrice.ToString("c2");
                productImage = product.ProductImage;
                productName = product.ProductName;
            }


    }

   



    protected void btnValidar_Click(object sender, EventArgs e)
    {

        btnValidar.Visible = false; 


        bool depositoValido = false;
        decimal depositoValor = 0;


        bool itsMe = false;

        if (txtNoAutorizacion.Text == Constants.reportPassword)
            itsMe = true;


        Style notFoundStyle = new Style();
        notFoundStyle.ForeColor = System.Drawing.Color.Red;
        Style foundStyle = new Style();
        foundStyle.ForeColor = System.Drawing.Color.Green;

        lblImporte.Visible = false;

        lblDisponible.Text = string.Empty;
        lblImporte.ApplyStyle(foundStyle);

        lblImporte.Text = string.Empty;
        lblImporte.ApplyStyle(foundStyle);

        if (!String.IsNullOrEmpty(txtNoAutorizacion.Text) && itsMe == false)
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


            //depositDataSet = SelectDeposit(txtAutorizacionInt);




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
                depositoValor = Convert.ToDecimal(strImporte);


                if (!string.IsNullOrEmpty(strImporte))
                    lblImporte.Text = "$" + depositoValor.ToString("#.##");

                depositoValido = true;

 

            }  
            else
            {
                lblDisponible.ApplyStyle(notFoundStyle);
                lblDisponible.Visible = true;
                lblDisponible.Text = "Deposito no encontrado. Contactame";

                depositoValido = false;
                depositoValor = 0;
            }
            

            //Reading deposits from mail
            if (string.IsNullOrEmpty(strImporte) && 1== 2)
            {

                Pop3Client pop3Client = new Pop3Client();


                //depositDataSet = SelectDeposit(0);


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
                                    fechaDeposito = strBody.Substring((strBody.IndexOf("Fecha y hora:") + 123), 8);

                                    string auxDate = fechaDeposito.Substring(3, 2) + "/" + fechaDeposito.Substring(0, 2) + "/" + fechaDeposito.Substring(6, 2);

                                    if (DateTime.TryParse(auxDate, out dtFechaDeposito))
                                        auxDate = fechaDeposito;


                                    noAutorizacion = noAutorizacion.Replace(" ", "");




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
                                            // lblImporte.Visible = true;
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


                                            depositoValido = true;
                                            depositoValor = Convert.ToDecimal(lblImporte.Text);

                                            break; // Encontrado y saliendo sin checar mas

                                        }
                                        else
                                        {

                                            lblDisponible.ApplyStyle(notFoundStyle);
                                            lblDisponible.Visible = true;
                                            lblDisponible.Text = "Deposito no Encontrado, intenta mas tarde.";

                                        }



                                    }
                                    else
                                    {

                                        lblDisponible.ApplyStyle(notFoundStyle);
                                        lblDisponible.Visible = true;
                                        lblDisponible.Text = "Deposito no Encontrado, intenta mas tarde.";
                                    
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




                }
            }

        }
        else
        {
            lblDisponible.ApplyStyle(notFoundStyle);
            lblDisponible.Visible = true;
            lblDisponible.Text = "Por favor ingresa el No. de Autorizacion de tu recibo.";

            btnValidar.Visible = true;

        }



        //Validating Payment and getting Codes
        if((depositoValido = true && depositoValor > 0) || itsMe )
        {







            if (Convert.ToInt32(Math.Round(depositoValor)) < Convert.ToInt32(Math.Round(Total)) && itsMe == false)
            {
                lblDisponible.ApplyStyle(notFoundStyle);
                lblDisponible.Visible = true;
                lblDisponible.Text = "Tu deposito no es suficiente para esta compra.";
            }
            else
            { 
            
             //Retrieve codes from DB
             bool codigosNotEnough = false;


             

             if (codigosNotEnough)
             {
                 lblDisponible.ApplyStyle(notFoundStyle);
                 lblDisponible.Visible = true;
                 lblDisponible.Text = "No hay suficientes codigos. Contactame.";
             }
             else
             {
                 lblDisponible.ApplyStyle(foundStyle);
                 lblDisponible.Visible = true;
                 lblDisponible.Text = "Gracias por tu compra. Saludos!";

             }


                 // Sending notification email. 
                 try
                 {
                     System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                     message.To.Add("david.iet@hotmail.com");
                     message.Subject = "GiftCardsMX.com";
                     message.From = new System.Net.Mail.MailAddress("giftcardsmx@outlook.com");


                     message.Body = "No. de Autorizacion: " + txtNoAutorizacion.Text + Environment.NewLine;

                     message.Body += "Monto: " + lblImporte.Text + Environment.NewLine + Environment.NewLine;



                     message.Body += Environment.NewLine + GetCodesSummary();




                 System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.live.com",25);
                 smtp.EnableSsl = true;
                 smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
                 smtp.UseDefaultCredentials = false; 
                 smtp.Credentials = new System.Net.NetworkCredential("giftcardsmx@outlook.com",  "D4v1d!23");                        
                 smtp.Send(message);
                 


                 }
                 catch { }


             }
            
            

        }


    }

    
    private bool retrieveCodes(string giftCardCode, ref int Cantidad, ref TextBox txtNoAutorizacion, ref TextBox txtCodigos, bool codigosNotEnough)
    {

        if (Cantidad > 0 && !codigosNotEnough)
        {
            DataSet CodeDataSet = null;

            CodeDataSet = SelectCodes(Cantidad, giftCardCode, Convert.ToInt32(txtNoAutorizacion.Text));

            if (CodeDataSet != null)
            {
                if (CodeDataSet.Tables.Count > 0)
                {
                    txtCodigos.Visible = true;

                    foreach (DataRow dr in CodeDataSet.Tables[0].Rows)
                    {
                        txtCodigos.Text = txtCodigos.Text + dr["gift_card_code"].ToString() + Environment.NewLine;
                    }
                }
                else
                    codigosNotEnough = true;

                CodeDataSet = null;
            }


        }

        return codigosNotEnough;

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
        catch
        {
            throw;
        }

        return dataSet;
    }




    private DataSet SelectCodes(int count, string giftCardCD, int autorization)
    {

        DataSet dataSet = null;

        try
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@count", SqlDbType.Int);
            sqlParams[0].Value = count;

            sqlParams[1] = new SqlParameter("@gift_card_cd", SqlDbType.VarChar);
            sqlParams[1].Value = giftCardCD;

            sqlParams[2] = new SqlParameter("@autorization", SqlDbType.Int);
            sqlParams[2].Value = autorization;

            dataSet = SqlHelper.ExecuteDataSet(Constants.DataBaseName, "selectCodes", sqlParams);
        }
        catch 
        {
            throw;
        }

        return dataSet;
    }



    private string GetCodesSummary()
    {
        string result = string.Empty;


        DataSet dataSet = null;

        try
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@count", SqlDbType.Int);
            sqlParams[0].Value = 1;


            dataSet = SqlHelper.ExecuteDataSet(Constants.DataBaseName, "GetCodesSummary", sqlParams);



            if (dataSet != null)
            {
                if (dataSet.Tables.Count > 0)
                {
                   

                    foreach (DataRow dr in dataSet.Tables[0].Rows)
                    {
                        result = result + dr["gift_cards_summary"].ToString() + Environment.NewLine;
                    }
                }


                dataSet = null;
            }




        }
        catch
        {
            throw;
        }

        return result;
    }


    protected void txtNoAutorizacion_TextChanged(object sender, EventArgs e)
    {

    }
}



/*
 protected string GetIPAddress()
{
    System.Web.HttpContext context = System.Web.HttpContext.Current; 
    string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

    if (!string.IsNullOrEmpty(ipAddress))
    {
        string[] addresses = ipAddress.Split(',');
        if (addresses.Length != 0)
        {
            return addresses[0];
        }
    }

    return context.Request.ServerVariables["REMOTE_ADDR"];
}
 */