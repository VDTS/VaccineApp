namespace Core.Models;

public class AccountInfoLookupModel
{
    public string? kind { get; set; }
    public List<User>? users { get; set; }
}
public class ProviderUserInfo
{
    public string? providerId { get; set; }
    public string? rawId { get; set; }
    public string? phoneNumber { get; set; }
    public string? photoUrl { get; set; }
    public string? displayName { get; set; }
    public string? federatedId { get; set; }
    public string? email { get; set; }
}

public class User
{
    public string? localId { get; set; }
    public string? email { get; set; }
    public string? displayName { get; set; }
    public string? passwordHash { get; set; }
    public bool emailVerified { get; set; }
    public long passwordUpdatedAt { get; set; }
    public List<ProviderUserInfo>? providerUserInfo { get; set; }
    public string? validSince { get; set; }
    public bool disabled { get; set; }
    public string? lastLoginAt { get; set; }
    public string? createdAt { get; set; }
    public string? phoneNumber { get; set; }
    public string? customAttributes { get; set; }
    public DateTime lastRefreshAt { get; set; }
}

public class CustomAttributes
{
    public string? Role { get; set; }
    public string? ClusterId { get; set; }
    public string? TeamId { get; set; }
    public string? FamilyId { get; set; }
}