	Identity Ýþlem Adýmlarý - Asp.Net Core Identity kütüphanesiyle
1-Nuget Package dan Identity kütüphanesi eklenir.
	-Microsoft.AspNetCore.Identity
	-Microsoft.AspNetCore.Identity.EntityFrameworkCore

2-User ve Role Modelleri eklenir
3-DbContext classýnýz IDentityDbContext<TUser, TRole, IKey> classýndan kalýtým alýr.
4-Migration iþlemi yapýlýr.
	Nuget Package Console da 
		-add-migration m
		-update-database
5-Program.cs de IdentityCookie ayarý yapýlýr.
6-Core katmanýnda service katmanlarý kullanýlýr. Classlarýn metotlarý async olur.
	-UserManager<User>
	-UserManager<Role>
	-SignManager<User>