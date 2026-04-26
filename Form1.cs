using System.Media;

namespace CharCounter;

public partial class Form1 : Form
{
    private readonly SortedDictionary<char, int> _counts = [];
    private readonly RowComparer _rowComparer = new();
    private string[] _fileNames = [];

    public Form1()
    {
        InitializeComponent();
        listView1.ListViewItemSorter = _rowComparer;
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
            ListViewItem.ListViewSubItem[] row =
            [
                new() { Text = key.ToString(), Tag = key.ToString() },
                new() { Text = ((int)key).ToString("x"), Tag = key },
                new() { Text = value.ToString(), Tag = value }
            ];

            _ = listView1.Items.Add(new ListViewItem(row, 0));
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

    private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (listView1.HitTest(e.X, e.Y).SubItem is not { Text: var character })
        {
            return;
        }

        Clipboard.SetText(character);
        SystemSounds.Beep.Play();
    }

    private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        if (_rowComparer.Index == e.Column)
        {
            _rowComparer.ReverseOrder ^= true;
        }
        else
        {
            _rowComparer.ReverseOrder = false;
            _rowComparer.Index = e.Column;
        }

        listView1.Sort();
    }
}
