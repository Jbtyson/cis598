namespace MusicGenerator.Music
{
    public enum Pitch
    {
        C4 = 72,
        D4 = 74,
        E4 = 76,
        F4 = 78,
        G4 = 80,
        A5 = 82,
        B5 = 84,
        C5 = 86,
    }

    public static class PitchCode
    {
        public static string GetNoteName(int pitchCode)
        {
            var val = "";
            switch(pitchCode)
            {
                case 72:
                    val = "C4";
                    break;
                case 74:
                    val = "D4";
                    break;
                case 76:
                    val = "E4";
                    break;
                case 78:
                    val = "F4";
                    break;
                case 80:
                    val = "G4";
                    break;
                case 82:
                    val = "A5";
                    break;
                case 84:
                    val = "B5";
                    break;
                case 86:
                    val = "C5";
                    break;
            }
            return val;
        }
    }
}
