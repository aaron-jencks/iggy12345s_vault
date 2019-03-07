using FireworkToolkit.Interfaces;
using FireworkToolkit.SpriteGraphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FireworkToolkit.Graphics.FormsComponents
{
    public partial class SpriteManager : Form
    {
        #region Properties

        /// <summary>
        /// The list of sprites currently in use by the manager
        /// </summary>
        public ICollection<Sprite> Sprites { get; private set; } = new List<Sprite>();
        public List<Sprite> edittingBuff { get; private set; } = new List<Sprite>();

        #endregion

        #region Events

        #region Sprite events

        public delegate void SpriteEventHandler(object sender, Sprite target);

        /// <summary>
        /// Triggered when a sprite is created from an image, or imported from an xml file
        /// </summary>
        public event SpriteEventHandler SpriteAdded;
        protected virtual void OnSpriteAdded(Sprite s)
        {
            SpriteAdded?.Invoke(this, s);
        }
        protected virtual void OnSpriteAdded(ICollection<Sprite> s)
        {
            foreach (Sprite sprite in s)
                OnSpriteAdded(sprite);
        }

        /// <summary>
        /// Triggered when one or more sprites are removed from the list
        /// </summary>
        public event SpriteEventHandler SpriteRemoved;
        protected virtual void OnSpriteRemoved(Sprite s)
        {
            SpriteRemoved?.Invoke(this, s);
        }
        protected virtual void OnSpriteRemoved(ICollection<Sprite> s)
        {
            foreach (Sprite sprite in s)
                OnSpriteRemoved(sprite);
        }

        /// <summary>
        /// Triggered every time that a sprite is edited, the target is the new sprite
        /// </summary>
        public event SpriteEventHandler SpriteEdited;
        protected virtual void OnSpriteEdited(Sprite s)
        {
            SpriteEdited?.Invoke(this, s);
        }

        #endregion

        public delegate void AcceptEventHandler(object sender, EventArgs e);

        public event AcceptEventHandler Accepted;
        protected virtual void OnAcceptedEvent()
        {
            Accepted?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new sprite manager with an empty list of sprites
        /// </summary>
        public SpriteManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new sprite manager with the given initial collection of sprites
        /// </summary>
        /// <param name="sprites"></param>
        public SpriteManager(ICollection<Sprite> sprites)
        {
            InitializeComponent();
            Sprites = sprites ?? new List<Sprite>();
            TransferSprites(false);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Transfers sprites either from or to the listbox
        /// </summary>
        /// <param name="fromListBox">Specifies whether to transfer the sprites from the listbox or to the listbox.</param>
        protected virtual void TransferSprites(bool fromListBox = true)
        {
            if(fromListBox)
            {
                Sprites.Clear();
                foreach (Sprite o in edittingBuff)
                    Sprites.Add((Sprite)o);
            }
            else
            {
                listBoxSprites.Items.Clear();
                foreach (Sprite s in Sprites)
                {
                    listBoxSprites.Items.Add(s.ToString());
                    edittingBuff.Add(s.Clone());
                }
                if (Sprites.Count > 0)
                {
                    listBoxSprites.SelectedIndex = 0;
                    spriteControl1.Value = edittingBuff[0];
                }
            }
        }

        #endregion

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            TransferSprites();
            OnAcceptedEvent();
            Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            OpenFileDialog wizard = new OpenFileDialog();
            wizard.Filter = "Bitmap | *.bmp";
            wizard.Title = "Select a sprite file to open";
            wizard.Multiselect = true;
            wizard.RestoreDirectory = true;
            wizard.ShowDialog();

            if (wizard.FileName != null)
                foreach (string s in wizard.FileNames)
                    if (s != "")
                    {
                        Sprite temp = new Sprite(s);
                        listBoxSprites.Items.Add(temp.ToString());
                        edittingBuff.Add(temp);
                        listBoxSprites.SelectedIndex = edittingBuff.IndexOf(temp);
                        OnSpriteAdded(temp);
                    }
        }

        private void buttonRemoveSelected_Click(object sender, EventArgs e)
        {
            List<object> temp = new List<object>(listBoxSprites.SelectedItems.Count);

            edittingBuff.RemoveAt(listBoxSprites.SelectedIndex);
            listBoxSprites.Items.RemoveAt(listBoxSprites.SelectedIndex);

            if (listBoxSprites.Items.Count > 0)
                listBoxSprites.SelectedIndex = 0;
        }

        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sprite o = edittingBuff[listBoxSprites.SelectedIndex];
            edittingBuff.RemoveAt(listBoxSprites.SelectedIndex);
            listBoxSprites.Items.RemoveAt(listBoxSprites.SelectedIndex);
            OnSpriteRemoved(o);
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Sprite o in edittingBuff)
                OnSpriteRemoved((Sprite)o);
            listBoxSprites.Items.Clear();
            edittingBuff.Clear();
        }

        private void fromBitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog wizard = new OpenFileDialog();
            wizard.Filter = "Bitmap | *.bmp";
            wizard.Title = "Select a sprite file to open";
            wizard.Multiselect = true;
            wizard.RestoreDirectory = true;
            wizard.ShowDialog();

            if(wizard.FileName != null)
                foreach(string s in wizard.FileNames)
                    if(s != "")
                    {
                        Sprite temp = new Sprite(s);
                        listBoxSprites.Items.Add(temp.ToString());
                        edittingBuff.Add(temp);
                        listBoxSprites.SelectedIndex = edittingBuff.IndexOf(temp);
                        OnSpriteAdded(temp);
                    }
        }

        private void fromXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog wizard = new OpenFileDialog();
            wizard.Filter = "Xml | *.xml";
            wizard.Title = "Select an attribute file to open";
            wizard.RestoreDirectory = true;
            wizard.ShowDialog();

            if (wizard.FileName != "")
            {
                XElement doc = XElement.Load(wizard.FileName);
                foreach (XElement child in doc.Elements())
                    switch (child.Name.ToString())
                    {
                        case "Sprite":
                            Sprite s = new Sprite();
                            s.FromElement(child);
                            OnSpriteAdded(s);
                            listBoxSprites.Items.Add(s.ToString());
                            edittingBuff.Add(s);
                            break;
                    }
            }

            wizard.Dispose();
        }

        private void toXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog wizard = new SaveFileDialog();
            wizard.AddExtension = true;
            wizard.Filter = "Xml | *.xml";
            wizard.DefaultExt = "xml";
            wizard.Title = "Select a file to save to";
            wizard.ShowDialog();

            if (wizard.FileName != "")
            {
                XElement doc = new XElement("root");
                TransferSprites();
                foreach (IFilable f in Sprites)
                    doc.Add(f.GetElement());
                doc.Save(wizard.FileName);
            }

            wizard.Dispose();
        }

        private void spriteControl1_ValueChanged(object sender, EventArgs e)
        {
            if (listBoxSprites.SelectedIndex > 0)
            {
                edittingBuff[listBoxSprites.SelectedIndex] = spriteControl1.Value;
                OnSpriteEdited(edittingBuff[listBoxSprites.SelectedIndex]);
            }
        }

        private void listBoxSprites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSprites.SelectedIndex < 0)
                spriteControl1.Value = null;
            else
                spriteControl1.Value = edittingBuff[listBoxSprites.SelectedIndex];
        }
    }
}
