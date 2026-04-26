using System.Media;

namespace CharCounter;

public partial class Form1 : Form
{
    private readonly Dictionary<char, (int Count, HashSet<string> FileNames)> _counts = [];
    private readonly RowComparer _rowComparer = new();
    private string[] _fileNames = [];

    public Form1()
    {
        InitializeComponent();
        listView1.ListViewItemSorter = _rowComparer;
    }

    private void ReadFile()
    {
        _counts.Clear();

        foreach (var fileName in _fileNames)
        {
            using StreamReader reader = new(fileName);

            while (reader.Read() is not -1 and var result)
            {
                var character = (char)result;

                if (!_counts.TryGetValue(character, out var pair))
                {
                    _counts.Add(character, (1, [fileName]));
                    continue;
                }

                ++pair.Count;
                _ = pair.FileNames.Add(fileName);
                _counts[character] = pair;
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
                new() { Text = value.Count.ToString(), Tag = value.Count },
                new() { Text = string.Join(", ", value.FileNames.Select(Path.GetFileName)), Tag = value.FileNames.Count }
            ];

            _ = listView1.Items.Add(new ListViewItem(row, 0));
        }

        foreach (var column in listView1.Columns.Cast<ColumnHeader>())
        {
            column.Width = -2;
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
