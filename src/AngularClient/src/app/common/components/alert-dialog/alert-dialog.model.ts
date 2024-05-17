export enum AlertDialogType {
  Info,
  Warning,
  Error
}

export class AlertDialogModel {
  title: string = '';
  message: string = '';
  type: AlertDialogType = AlertDialogType.Info;

  constructor(value: {
    title: string,
    message: string,
    type?: AlertDialogType
  }) {
    Object.assign(this, value);
  }
}
