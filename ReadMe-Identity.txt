	Identity ��lem Ad�mlar� - Asp.Net Core Identity k�t�phanesiyle
1-Nuget Package dan Identity k�t�phanesi eklenir.
	-Microsoft.AspNetCore.Identity
	-Microsoft.AspNetCore.Identity.EntityFrameworkCore

2-User ve Role Modelleri eklenir
3-DbContext class�n�z IDentityDbContext<TUser, TRole, IKey> class�ndan kal�t�m al�r.
4-Migration i�lemi yap�l�r.
	Nuget Package Console da 
		-add-migration m
		-update-database
5-Program.cs de IdentityCookie ayar� yap�l�r.
6-Core katman�nda service katmanlar� kullan�l�r. Classlar�n metotlar� async olur.
	-UserManager<User>
	-UserManager<Role>
	-SignManager<User>