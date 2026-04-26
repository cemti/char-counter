using System.Media;

namespace CharCounter;

public partial class Form1 : Form
{
    private readonly SortedDictionary<char, int> _counts = [];

    public Form1()
    {
        InitializeComponent();
    }

    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new()
        {
            Title = "Open file",
            CheckFileExists = true,
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        using var file = dialog.OpenFile();
        using StreamReader reader = new(file);

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

        listBox1.Items.Clear();

        foreach (var (key, value) in _counts)
        {
            _ = listBox1.Items.Add($"{key} ({(int)key:x}) -> {value}");
        }
    }

    private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBox1.SelectedItem is not string item)
        {
            return;
        }

        Clipboard.SetText(item[..1]);
        SystemSounds.Beep.Play();
    }
}
