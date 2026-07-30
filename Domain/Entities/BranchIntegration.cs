namespace Domain.Entities;

public class BranchIntegration
{
    // Required by EF Core
    private BranchIntegration()
    {
    }

    public int BranchId { get; private set; }

    public string? TwilioMobile { get; private set; }

    public string? MailGunEmail { get; private set; }

    public string? GoCardlessOrganizationId { get; private set; }

    public string? TwilioPhoneNumberForVoice { get; private set; }

    public string? WhatsAppPhoneNumber { get; private set; }

    public static BranchIntegration Create(
        int branchId,
        string? twilioMobile = null,
        string? mailGunEmail = null,
        string? goCardlessOrganizationId = null,
        string? twilioPhoneNumberForVoice = null,
        string? whatsAppPhoneNumber = null)
    {
        return new BranchIntegration
        {
            BranchId = branchId,
            TwilioMobile = twilioMobile,
            MailGunEmail = mailGunEmail,
            GoCardlessOrganizationId = goCardlessOrganizationId,
            TwilioPhoneNumberForVoice = twilioPhoneNumberForVoice,
            WhatsAppPhoneNumber = whatsAppPhoneNumber
        };
    }

    public void UpdateTwilioMobile(string? twilioMobile)
    {
        TwilioMobile = twilioMobile;
    }

    public void UpdateMailGunEmail(string? mailGunEmail)
    {
        MailGunEmail = mailGunEmail;
    }

    public void UpdateGoCardlessOrganization(string? organizationId)
    {
        GoCardlessOrganizationId = organizationId;
    }

    public void UpdateVoiceNumber(string? voiceNumber)
    {
        TwilioPhoneNumberForVoice = voiceNumber;
    }

    public void UpdateWhatsAppNumber(string? whatsAppNumber)
    {
        WhatsAppPhoneNumber = whatsAppNumber;
    }
}