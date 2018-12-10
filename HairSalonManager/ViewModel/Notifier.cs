using System.ComponentModel;

public class Notifier : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged; /*프로퍼티가 바뀌는걸 감지 --> 새롭게 업데이트 시켜주는 클래스*/

    protected void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
