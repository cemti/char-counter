using System.Diagnostics;
using System.Media;
using System.Text;

namespace CharCounter;

public partial class Form1 : Form
{
    private readonly Dictionary<char, OrderedDictionary<string, int>> _counts = [];
    private readonly List<Input> _inputs = [];
    private readonly RowComparer _rowComparer = new();

    public Form1()
    {
        InitializeComponent();
        listView1.ListViewItemSorter = _rowComparer;
    }

    private void ReadStream(string fileName, Stream stream)
    {
        using StreamReader reader = new(stream);

        while (reader.Read() is not -1 and var result)
        {
            var character = (char)result;

            if (!_counts.TryGetValue(character, out var dictionary))
            {
                _counts.Add(character, new() { { fileName, 1 } });
                continue;
            }

            if (dictionary.TryGetValue(fileName, out var count))
            {
                dictionary[fileName] = count + 1;
                continue;
            }

            dictionary.Add(fileName, 1);
        }
    }

    private void ReadInput(Input input)
    {
        string fileName;
        Stream stream;

        switch (input)
        {
            case InputFile { FileName: var inputFileName }:
                fileName = inputFileName;
                stream = File.OpenRead(inputFileName);
                break;

            case InputText { Text: var text }:
                fileName = $"<clipboard-{text.GetHashCode():x}>";
                var array = Encoding.UTF8.GetBytes(Clipboard.GetText());
                stream = new MemoryStream(array);
                break;

            default:
                throw new UnreachableException();
        }

        ReadStream(fileName, stream);
    }

    private void RefreshListBox()
    {
        listView1.Items.Clear();

        foreach (var (key, value) in _counts)
        {
            var row = new ListViewItem.ListViewSubItem[]
            {
                new() { Text = key.ToString(), Tag = key.ToString() },
                new() { Text = ((int)key).ToString("x"), Tag = key },
                null!,
                null!
            };

            var total = value.Values.Sum();
            row[2] = new() { Text = total.ToString(), Tag = total };

            var text = string.Join(", ", value.Select(x => $"{Path.GetFileName(x.Key)} ({x.Value})"));
            row[3] = new() { Text = text, Tag = value.Count };

            _ = listView1.Items.Add(new ListViewItem(row, 0));
        }

        foreach (var column in listView1.Columns.Cast<ColumnHeader>())
        {
            column.Width = -2;
        }
    }

    private void ClearAll()
    {
        _counts.Clear();
        _inputs.Clear();
        listView1.Items.Clear();
    }

    private void RefreshAll()
    {
        _counts.Clear();
        _inputs.ForEach(ReadInput);
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

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            foreach (var fileName in dialog.FileNames)
            {
                InputFile input = new(fileName);
                _inputs.Add(input);
                ReadInput(input);
            }

            RefreshListBox();
        }
    }

    private void RefreshToolStripMenuItem_Click(object sender, EventArgs e) => RefreshAll();

    private void PasteTextFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
        InputText input = new(Clipboard.GetText());
        _inputs.Add(input);
        ReadInput(input);
        RefreshListBox();
    }

    private void ClearListToolStripMenuItem_Click(object sender, EventArgs e) => ClearAll();

    private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (listView1.HitTest(e.X, e.Y).SubItem is { Text: var character })
        {
            Clipboard.SetText(character);
            SystemSounds.Beep.Play();
        }
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
