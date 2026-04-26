using System.Media;

namespace CharCounter;

public partial class Form1 : Form
{
    private readonly SortedDictionary<char, int> _counts = [];
    private string[] _fileNames = [];

    public Form1()
    {
        InitializeComponent();
    }

    private void ReadFile()
    {
        foreach (var fileName in _fileNames)
        {
            using StreamReader reader = new(fileName);
            _counts.Clear();

            while (reader.Read() is not -1 and var result)
            {
                var character = (char)result;

                if (_counts.TryGetValue(character, out int value))
                {
                    _counts[character] = value + 1;
                    continue;
                }

                _counts.Add(character, 1);
            }
        }
    }

    private void RefreshListBox()
    {
        listView1.Items.Clear();

        foreach (var (key, value) in _counts)
        {
            string[] row = [key.ToString(), ((int)key).ToString("x"), value.ToString()];
            _ = listView1.Items.Add(new ListViewItem(row));
        }
    }

    private void RefreshAll()
    {
        ReadFile();
        RefreshListBox();
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new()
        {
            Title = "Open file",
            CheckFileExists = true,
            Multiselect = true
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        _fileNames = dialog.FileNames;
        RefreshAll();
    }

    private void RefreshToolStripMenuItem_Click(object sender, EventArgs e) => RefreshAll();

    private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listView1.SelectedItems is not [{ SubItems: [{ Text: var character }, ..] }])
        {
            return;
        }

        Clipboard.SetText(character);
        SystemSounds.Beep.Play();
    }
}
