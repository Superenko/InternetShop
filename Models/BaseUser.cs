using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kursova.Models
{
    public abstract class BaseUser : INotifyPropertyChanged
    {
        private static int s_idGenerator = 0;

        public int ID { get; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => _password = value;
        }

        private int _balance;
        public int Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Amount of money should be positive!");
                }
                _balance = value;
                OnPropertyChanged();
            }
        }

        public BaseUser(string name, string password)
        {
            ID = ++s_idGenerator;
            Name = name;
            Password = password;
            Balance = 0;
        }

        public void MakeDeposit(int value)
        {
            Balance += value;
        }

        public abstract void MakePurchase(Product product, int quantity);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
