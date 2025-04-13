import api from './api';

export const downloadClassAssignmentReportPdf = async (
  classId: number,
  assignmentSetId: number,
) => {
  const response = await api.get('/reports/classassignments', {
    params: { educationClassId: classId, assignmentSetId },
    responseType: 'blob',
  });

  const url = window.URL.createObjectURL(new Blob([response.data], { type: 'application/pdf' }));
  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', `class-report-${assignmentSetId}.pdf`);
  document.body.appendChild(link);
  link.click();
  link.remove();
};
