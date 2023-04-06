import moment, { Moment } from 'moment';
export function formatDate(date: string, standard: boolean = true): string {
    let data = new Date(date)
    const momentDate: Moment = moment(data);
    if (standard) {
      return momentDate.format('DD/MM/YYYY');
    }
    return momentDate.format('YYYY-MM-DD');
  }

  export function formatDateToStr(date: Date | null): string {   
    const momentDate: Moment = moment(date);   
    return momentDate.format('YYYY-MM-DD');
  }