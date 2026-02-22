import React, { useState, useEffect } from 'react';

const TaskForm = ({ currentTask, onSave, onCancel }) => {
    const [task, setTask] = useState({
        titulo: '',
        descricao: '',
        status: 0 // Default: Pendente
    });

    useEffect(() => {
        if (currentTask) {
            setTask({
                ...currentTask,
                status: parseInt(convertStatusToEnum(currentTask.status))
            });
        }
    }, [currentTask]);

    // Helper to map string status from API back to Enum INT for editing
    const convertStatusToEnum = (statusString) => {
        if (statusString === 'EmProgresso') return 1;
        if (statusString === 'Concluida') return 2;
        return 0; // Pendente
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSave(task);
    };

    return (
        <div className="card mb-4">
            <div className="card-header">
                {currentTask ? 'Editar Tarefa' : 'Nova Tarefa'}
            </div>
            <div className="card-body">
                <form onSubmit={handleSubmit}>
                    <div className="mb-3">
                        <label className="form-label">Título</label>
                        <input
                            type="text"
                            className="form-control"
                            required
                            maxLength="100"
                            value={task.titulo}
                            onChange={(e) => setTask({ ...task, titulo: e.target.value })}
                        />
                    </div>
                    <div className="mb-3">
                        <label className="form-label">Descrição</label>
                        <textarea
                            className="form-control"
                            value={task.descricao || ''}
                            onChange={(e) => setTask({ ...task, descricao: e.target.value })}
                        />
                    </div>
                    
                    {/* Only show Status if editing */}
                    {currentTask && (
                        <div className="mb-3">
                            <label className="form-label">Status</label>
                            <select
                                className="form-select"
                                value={task.status}
                                onChange={(e) => setTask({ ...task, status: parseInt(e.target.value) })}
                            >
                                <option value={0}>Pendente</option>
                                <option value={1}>Em Progresso</option>
                                <option value={2}>Concluída</option>
                            </select>
                        </div>
                    )}

                    <button type="submit" className="btn btn-primary me-2">Salvar</button>
                    <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancelar</button>
                </form>
            </div>
        </div>
    );
};

export default TaskForm;