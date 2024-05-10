export class ConfirmDialogModel {
  message: string = 'Please, confirm this action';
  title: string = 'Confirm action';
  yesButtonText: string = 'Yes';
  noButtonText: string = 'No';

  constructor(value: {
    message?: string,
    title?: string,
    yesButtonText?: string,
    noButtonText?: string
  }) {
    Object.assign(this, value);
  }
}
