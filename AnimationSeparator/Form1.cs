using System.Windows.Forms;

namespace AnimationSeparator
{
    public partial class Form1 : Form
    {
        string file;
        public Form1()
        {
            file = "";
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //��������
            OpenFileDialog ofg = new OpenFileDialog();
            //����Ĭ�ϴ�·��
            ofg.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath)) + "\\assets\\animation";
            //���ô򿪱��⡢��׺
            ofg.Title = "��ѡ���붯���ļ�";
            ofg.Filter = "png�ļ�|*.png";
            string path = "";
            DialogResult s = ofg.ShowDialog();
            if (s == DialogResult.OK)
            {
                //�õ��򿪵��ļ�·���������ļ�����
                file = ofg.FileName;
                label1.Text = file;
            }
            else if (s == DialogResult.Cancel)
                MessageBox.Show("δѡ����ļ���");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (file == "")
            {
                MessageBox.Show("δѡ����ļ���");
                return;
            }
            Image img = Image.FromFile(file);
            if (img.Width % 192 != 0 || img.Height % 192 != 0)
            {
                MessageBox.Show("�ļ�������RMXP�淶��192x192����");
                return;
            }
            string readyfile = file.Remove(file.LastIndexOf(".png"));
            int row = img.Height / 192, col = img.Width / 192;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Rectangle cropRegion = new Rectangle(j * 192, i * 192, 192, 192);
                    Bitmap result = new Bitmap(192, 192);
                    Graphics graphics = Graphics.FromImage(result);
                    graphics.DrawImage(img, new Rectangle(0, 0, 192, 192), cropRegion, GraphicsUnit.Pixel);
                    result.Save(readyfile + "_" + (i * col + j).ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            MessageBox.Show("������ɣ�");
        }
    }
}