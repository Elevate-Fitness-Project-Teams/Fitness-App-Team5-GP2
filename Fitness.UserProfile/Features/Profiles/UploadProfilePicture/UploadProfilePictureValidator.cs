using FluentValidation;

namespace Fitness.UserProfile.Features.Profiles.UploadProfilePicture;

public class UploadProfilePictureValidator : AbstractValidator<UploadProfilePictureCommand>
{
    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB
    private static readonly string[] AllowedContentTypes = { "image/jpeg", "image/jpg", "image/png" };

    public UploadProfilePictureValidator()
    {
        RuleFor(x => x.Length)
            .GreaterThan(0).WithMessage("File is empty.")
            .LessThanOrEqualTo(MaxFileSizeBytes).WithMessage("File size must not exceed 5 MB.");

        RuleFor(x => x.ContentType)
            .Must(ct => AllowedContentTypes.Contains(ct))
            .WithMessage("Only JPG and PNG images are allowed.");
    }
}
