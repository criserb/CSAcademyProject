namespace ProjectAcademy
{
    class Record
    {
        public string Nick { get; set; }
        public int Time { get; set; }
        public string Dimension { get; set; }
        public int Number { get; set; }
        public Record(string nick, int time, string dimension)
        {
            Nick = nick;
            Time = time;
            Dimension = dimension;
        }
    }
}
