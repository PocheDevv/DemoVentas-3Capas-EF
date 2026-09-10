<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="SitioWEB_VentasGUI.Logout" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sesión Cerrada</title>
    <!-- CSS de Bootstrap y FontAwesome -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet" />
    <!-- Redirige automáticamente al Login después de 7 segundos de mostrar el mensaje -->
<meta http-equiv="refresh" content="7;url=Login.aspx" />
</head>
<body class="bg-light d-flex align-items-center justify-content-center" style="height: 100vh;">
    <form id="form1" runat="server">
        <div class="card shadow text-center" style="max-width: 400px; border-radius: 15px;">
            <div class="card-body p-5">
                <!-- Icono de Candado o Check Seguro -->
                <div class="text-danger mb-4">
                    <i class="fas fa-user-shield fa-4x"></i>
                </div>
                
                <h3 class="card-title fw-bold mb-3" style="color: #993366;">¡Sesión Finalizada!</h3>
                <p class="card-text text-muted mb-4">
                    Has salido del sistema de manera segura. Tu información y consultas pendientes han sido protegidas.
                </p>
                
                <!-- Botón de Redirección Directa -->
                <a href="Login.aspx" class="btn w-100 fw-bold" style="background-color: #993366; color: white;">
                    <i class="fas fa-sign-in-alt me-2"></i> Iniciar Sesión Otra Vez
                </a>
            </div>
        </div>
    </form>
</body>
</html>