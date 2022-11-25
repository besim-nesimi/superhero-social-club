using System;
using System.Collections.Generic;

namespace superhero_social_club.Models;

public partial class Superhero
{
    public int SuperheroId { get; set; }

    public string SuperheroName { get; set; } = null!;

    public string Superpower { get; set; } = null!;

    public string SecretIdenty { get; set; } = null!;

    public string? ImageReference { get; set; }
}
