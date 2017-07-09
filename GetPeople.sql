CREATE PROCEDURE dbo.GetPeople
AS
BEGIN

SELECT PersonID
	,FullName
	,PreferredName
	,SearchName
	,IsPermittedToLogon
	,LogonName
	,IsExternalLogonProvider
	,HashedPassword
	,IsSystemUser
	,IsEmployee
	,IsSalesperson
	,UserPreferences
	,PhoneNumber
	,FaxNumber
	,EmailAddress
	,Photo
	,CustomFields
	,OtherLanguages
	,LastEditedBy
	,ValidFrom
	,ValidTo
FROM [Application].People;

END;
GO