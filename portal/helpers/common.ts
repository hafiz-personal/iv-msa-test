import moment, { Moment } from 'moment';
export function formatDate(date: string): string {
    let data = new Date(date)
    const momentDate: Moment = moment(data);
    return momentDate.format('DD/MM/YYYY');
  }
  