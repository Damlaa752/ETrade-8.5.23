
1- Account > SignIn View'�na google ile ba�lan linki olu�turulur.
	<a class="btn btn-primary" asp-controller="Account" asp-action="ExternalLogin" asp-route-provider="Google"> Google </a>

2- Google ve Identity Paketleri y�klenir.
	* Microsoft.AspNetCore.Authotication.Google
	* Microsoft.AspNetCore.Identity

3- Google API den ClientId ve ClientSecret al�n�r.
	Google Developer Console da bir proje olu�turulur.
	Kimlik do�rulama url eklenir.
	http:localhost:port/signin-google
4- Program.cs e AddAuthentication.AddGoogle option ayar� 
	AddIdentity options

5- Account Controllers giri� i�in gerekli action yaz�l�r.

