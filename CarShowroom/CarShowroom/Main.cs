﻿using CarShowroom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarShowroom
{
    public partial class Main : Form
    {
        Showroom showroom;

        public Main()
        {
            InitializeComponent();

            showroom = new Showroom();
            try
            {
                showroom.Load();
                carBindingSource.DataSource = showroom.Cars;
                clientBindingSource.DataSource = showroom.Clients;
                carBindingSource.ResetBindings(false);
                clientBindingSource.ResetBindings(false);
            }
            catch (Exception)
            {

            }
        }

        private void Cars_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!showroom.IsDirty)
                return;
            var res = MessageBox.Show("Save data before exit?", "", MessageBoxButtons.YesNoCancel);
            switch (res)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    showroom.Save();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showroom.Load();
            carBindingSource.ResetBindings(false);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showroom.Save();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cf = new CarForm();
            if (cf.ShowDialog() == DialogResult.OK)
            {
                showroom.AddCar(cf.Car);
                carBindingSource.ResetBindings(false);
                showroom.IsDirty = true;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toEdit = carlistGridView.SelectedRows[0].DataBoundItem as Car;
            var cf = new CarForm(toEdit);
            if (cf.ShowDialog() == DialogResult.OK)
            {
                carBindingSource.ResetBindings(false);
                showroom.IsDirty = true;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var toDel = carlistGridView.SelectedRows[0].DataBoundItem as Car;
            var res = MessageBox.Show($"Delete {toDel.Brand} ?", "", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                showroom.Cars.Remove(toDel);
                carBindingSource.ResetBindings(false);
                showroom.IsDirty = true;
            }
        }

        private void newClientToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var clf = new Registration();
            if (clf.ShowDialog() == DialogResult.OK)
            {
                showroom.AddClient(clf.client);
                clientBindingSource.ResetBindings(false);
                showroom.IsDirty = true;
            }
        }

        private void editCLientToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var toEdit = clientsGridView.SelectedRows[0].DataBoundItem as Client;
            var clf = new Registration(toEdit);
            if (clf.ShowDialog() == DialogResult.OK)
            {
                clientBindingSource.ResetBindings(false);
                showroom.IsDirty = true;
            }
        }

        private void deleteClientToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var toDel = clientsGridView.SelectedRows[0].DataBoundItem as Client;
            var res = MessageBox.Show($"Delete {toDel.Brand} ?", "", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                showroom.Clients.Remove(toDel);
                clientBindingSource.ResetBindings(false);
                showroom.IsDirty = true;
            }
        }
    }
}
