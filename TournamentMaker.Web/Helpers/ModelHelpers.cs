using System;
using TournamentReport.Models;

public static class ModelHelpers
{
    public static int GetYear(this Tournament tournament)
    {
        // HACK: This is some hackish shit to workaround our lack of a date on Tournament. 
        // Fix this when migrations work again.
        var name = tournament.Name;
        if (String.IsNullOrEmpty(name) || name.Length < 4) return 0;
        try
        {
            return Convert.ToInt32(name.Substring(0, 4));
        }
        catch (Exception)
        {
            return 0;
        }
    }
}