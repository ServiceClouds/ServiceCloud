using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class BranchIntegration
{
    public int BranchId { get; set; }

    public string? TwilioMobile { get; set; }

    public string? MailGunEmail { get; set; }

    public string? GoCardlessOrganizationId { get; set; }

    public string? TwilioPhoneNumberForVoice { get; set; }

    public string? WhatsAppPhoneNumber { get; set; }
}
