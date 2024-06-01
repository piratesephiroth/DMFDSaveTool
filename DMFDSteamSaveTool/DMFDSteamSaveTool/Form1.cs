using System;
using System.IO;
using System.Windows.Forms;

namespace DMFDSteamSaveTool
{
    public partial class Form1 : Form
    {
        string halfPassword0 = "gYjkJoTX";
        string halfPassword1 = "zZ2c9VTK";
        string encFilePath = "";
        string decFilePath = "";
        string outputDecFilePath = "";
        string outputEncFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }


        private bool ValidSteamID (string steamID)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(steamID, "[^0-9]"))
            {
                return false;
            }

            return true;
        }


        //
        // load input files
        //
        private void browseEnc_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                //InitialDirectory = @"D:\",
                Title = "Select the encrypted save file",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "bin",
                Filter = "DMFD save files (*.bin)|*.bin",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                encFilePath = openFileDialog1.FileName;
                encFileNameLabel.Text = Path.GetFileName(encFilePath);
                decrypt_button.Enabled = true;
            }
        }

        private void browseDec_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                //InitialDirectory = @"D:\",
                Title = "Select the decrypted save file",

                //CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "bin",
                Filter = "DMFD save files (*.bin)|*.bin",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                decFilePath = openFileDialog1.FileName;
                decFileNameLabel.Text = Path.GetFileName(decFilePath);
                encrypt_button.Enabled = true;
            }
        }

        //
        // save output files
        //
        private void decrypt_button_Click(object sender, EventArgs e)
        {
            if (ValidSteamID(steamID64_enc.Text))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save the decrypted save file",
                    CheckPathExists = true,
                    DefaultExt = "bin",
                    Filter = "DMFD save files (*.bin)|*.bin",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputDecFilePath = saveFileDialog.FileName;

                    byte[] saveData;

                    // load encrypted file and read its contents into byte array
                    using (BinaryReader inFile = new BinaryReader(File.OpenRead(encFilePath)))
                    {
                        inFile.BaseStream.Position = 0x10;  // skip header
                        saveData = inFile.ReadBytes((int)(inFile.BaseStream.Length - 0x10));
                    }

                    // Steam
                    if (steamID64_enc.Text.Length > 0)
                    {
                        ulong steamID = ulong.Parse(steamID64_enc.Text) & 0xFFFFFFFF;
                        dmfd.Decrypt(halfPassword0 + steamID.ToString("x"), ref saveData);
                        dmfd.Decrypt(halfPassword1 + steamID.ToString("x"), ref saveData);
                    }
                    // NS (maybe PS4 too?)
                    else
                    {
                        dmfd.Decrypt(halfPassword0, ref saveData);
                        dmfd.Decrypt(halfPassword1, ref saveData);
                    }

                    FileMode openMode;
                    if (File.Exists(outputDecFilePath))
                        openMode = FileMode.Truncate;
                    else
                        openMode = FileMode.CreateNew;

                    // save
                    using (BinaryWriter outFile = new BinaryWriter(File.Open(outputDecFilePath, openMode, FileAccess.ReadWrite)))
                    {
                        outFile.Write(saveData);
                    }

                    MessageBox.Show($"{Path.GetFileName(outputDecFilePath)} saved successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else
            {
                MessageBox.Show("Please enter a Steam ID.\nOnly numbers are accepted.", "Invalid Steam ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void encrypt_button_Click(object sender, EventArgs e)
        {
            if (ValidSteamID(steamID64_dec.Text))
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save the encrypted save file",
                    CheckPathExists = true,
                    DefaultExt = "bin",
                    Filter = "DMFD save files (*.bin)|*.bin",
                    FilterIndex = 2,
                    RestoreDirectory = true,
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputEncFilePath = saveFileDialog.FileName;
                    // load decrypted file, encrypt and save it

                    // header (0x10 bytes)
                    uint magic = 0xAE9F2304;
                    uint unknown = 0x68;
                    uint chksum0 = 0;
                    uint chksum1 = 0;
                    byte[] saveData;

                    // read decrypted data
                    saveData = File.ReadAllBytes(decFilePath);

                    // calculate 1st checksum
                    chksum0 = dmfd.Checksum(saveData);

                    // encrypt for Steam
                    if (steamID64_dec.Text.Length > 0)
                    {
                        ulong steamID = ulong.Parse(steamID64_dec.Text) & 0xFFFFFFFF;
                        dmfd.Encrypt(halfPassword1 + steamID.ToString("x"), ref saveData);
                        dmfd.Encrypt(halfPassword0 + steamID.ToString("x"), ref saveData);
                    }
                    // encrypt for NS (PS4 too?)
                    else
                    {
                        dmfd.Encrypt(halfPassword1, ref saveData);
                        dmfd.Encrypt(halfPassword0, ref saveData);
                    }
                            

                    // calculate 2nd checksum
                    chksum1 = dmfd.Checksum(saveData);

                    FileMode openMode;
                    if (File.Exists(outputEncFilePath))
                        openMode = FileMode.Truncate;
                    else
                        openMode = FileMode.CreateNew;

                    // save encrypted file
                    using (BinaryWriter outFile = new BinaryWriter(File.Open(outputEncFilePath, openMode, FileAccess.ReadWrite)))
                    {
                        outFile.Write(magic);
                        outFile.Write(unknown);
                        outFile.Write(chksum0);
                        outFile.Write(chksum1);
                        outFile.Write(saveData);
                    }

                    MessageBox.Show($"{Path.GetFileName(outputEncFilePath)} saved successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }

            else
            {
                MessageBox.Show("Please enter a Steam ID.\nOnly numbers are accepted.", "Invalid Steam ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
