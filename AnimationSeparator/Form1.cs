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
            //创建对象
            OpenFileDialog ofg = new OpenFileDialog();
            //设置默认打开路径
            ofg.InitialDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath)) + "\\assets\\animation";
            //设置打开标题、后缀
            ofg.Title = "请选择导入动画文件";
            ofg.Filter = "png文件|*.png";
            string path = "";
            DialogResult s = ofg.ShowDialog();
            if (s == DialogResult.OK)
            {
                //得到打开的文件路径（包括文件名）
                file = ofg.FileName;
                label1.Text = file;
            }
            else if (s == DialogResult.Cancel)
                MessageBox.Show("未选择打开文件！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (file == "")
            {
                MessageBox.Show("未选择打开文件！");
                return;
            }
            Image img = Image.FromFile(file);
            if (img.Width % 192 != 0 || img.Height % 192 != 0)
            {
                MessageBox.Show("文件不符合RMXP规范（192x192）！");
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
            MessageBox.Show("制作完成！");
        }
    }
}