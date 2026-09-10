<%@ Page Title="" Language="C#" MasterPageFile="~/MasterModelo.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SitioWEB_VentasGUI.Default" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cabecera" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Principal" runat="server">
    <style>
        body {
            background-color: #f8f9fa; /* Color de fondo claro */
        }
        .navbar-brand {
            font-weight: bold;
        }
        .card-icon {
            font-size: 2rem;
            color: #0d6efd; /* Color del icono, ajusta al tema */
        }
        .chart-container {
            position: relative;
            height: 40vh; /* Altura responsiva para el gráfico */
            width: 100%;
        }
        /* Ajuste de márgenes para el footer */
        footer {
            margin-top: 3rem;
            padding-top: 1.5rem;
            padding-bottom: 1.5rem;
            border-top: 1px solid #e9ecef;
            background-color: #f2f2f2;
            color: #6c757d;
        }
    </style>
   <nav class="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm">
    <div class="container-fluid">
        <a class="navbar-brand" href="#">
            <img src="Images/LeonCorporation.png" alt="Logo de la Empresa" width="200" height="100" class="d-inline-block align-text-top me-2">
            Bienvenidos al modulo WEB - Sistema de Ventas Leon Corporation S.A.
        </a>
        </div>
</nav>
         <div class="container mt-4">
            <h1 class="mb-4 text-center">Dashboard de Ventas</h1>

            <div class="row mb-4">
                <div class="col-md-4">
                    <div class="card text-center shadow-sm h-100">
                        <div class="card-body">
                            <h5 class="card-title text-primary"><i class="bi bi-currency-dollar card-icon"></i> Total Ventas</h5>
                            <p class="card-text fs-3"><asp:Literal ID="litTotalVentas" runat="server" Text="$0.00"></asp:Literal></p>
                            
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-center shadow-sm h-100">
                        <div class="card-body">
                            <h5 class="card-title text-success"><i class="bi bi-people-fill card-icon"></i> Clientes activos</h5>
                            <p class="card-text fs-3"><asp:Literal ID="litClientesActivos" runat="server" ></asp:Literal></p>
                           
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-center shadow-sm h-100">
                        <div class="card-body">
                            <h5 class="card-title text-warning"><i class="bi bi-box-seam-fill card-icon"></i> Total deuda a la fecha</h5>
                            <p class="card-text fs-3">                               
                                      <asp:Literal ID="litDeudaActual" runat="server"  ></asp:Literal>
                            </p>
                          
                        </div>
                    </div>
                </div>
            </div>

            <div class="row mb-4">
                <div class="col-12">
                    <div class="card shadow-sm">
                        <div class="card-header bg-white">
                            <h5 class="mb-0">Estadisticas anuales</h5>
                        </div>
                        <div class="card-body">
                            <div class="chart-container">
                                  <table class="table">                                      
                                            <td align="center">
                                               <%--AQUI VA grafTotales--%>
                                                <asp:Chart ID="grafTotales" runat="server" Width="401px">
                                                   <Series>
                                                       <asp:Series Name="Series1"></asp:Series>
                                                   </Series>
                                                   <ChartAreas>
                                                       <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                                   </ChartAreas>
                                                    <Titles>
                                                        <asp:Title BackColor="Silver" Font="Microsoft Sans Serif, 8pt, style=Bold" Name="Title1" Text="Total de Ventas">
                                                        </asp:Title>
                                                    </Titles>
                                               </asp:Chart>
                                           </td>
                                
                                 <td  align="center">
                                          <%--AQUI VA grafCantidad--%>
                                     <asp:Chart ID="grafCantidad" runat="server" Width="400px">
                                         <Series>
                                             <asp:Series Name="Series1"></asp:Series>
                                         </Series>
                                         <ChartAreas>
                                             <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                                         </ChartAreas>
                                         <Titles>
                                             <asp:Title BackColor="Silver" Font="Microsoft Sans Serif, 8pt, style=Bold" Name="Title1" Text="Cantidad de facturas anuales">
                                             </asp:Title>
                                         </Titles>
                                     </asp:Chart>
                                </td>
   
                                </table>
                            
                            </div>
                        </div>
                    </div>
                </div>
            </div>        
        </div>

       
    </asp:Content>
