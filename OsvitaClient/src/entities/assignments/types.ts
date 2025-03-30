export interface Assignment {
  id: number;
  objectId: number;
  problemDescription: string;
  explanation: string;
  assignmentModelType: number;
  parentAssignmentId: number;
  answers: [
    { id: number; value: string; isCorrect: boolean; points: number; assignmentId: number },
  ];
  childAssignments: [];
}
