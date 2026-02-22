import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap styles
import { getTasks, createTask, updateTask, deleteTask } from './api';
import TaskForm from './components/TaskForm';

function App() {
    const [tasks, setTasks] = useState([]);
    const [filter, setFilter] = useState('Todos'); // [cite: 32]
    const [editingTask, setEditingTask] = useState(null);
    const [isFormVisible, setIsFormVisible] = useState(false);

    // Load Tasks
    const loadTasks = async () => {
        try {
            const response = await getTasks();
            setTasks(response.data);
        } catch (error) {
            console.error("Erro ao carregar tarefas", error);
        }
    };

    useEffect(() => {
        loadTasks();
    }, []);

    // Filter Logic
    const filteredTasks = tasks.filter(task => {
        if (filter === 'Todos') return true;
        return task.status === filter; // API returns status as String (Pendente, etc.)
    });

    // Handlers
    const handleSave = async (taskData) => {
        try {
            if (editingTask) {
                await updateTask(editingTask.id, taskData);
            } else {
                await createTask(taskData);
            }
            setIsFormVisible(false);
            setEditingTask(null);
            loadTasks(); // Refresh list
        } catch (error) {
            alert('Erro ao salvar: ' + (error.response?.data || error.message));
        }
    };

    const handleDelete = async (id) => {
        if (window.confirm('Tem certeza que deseja excluir?')) {
            await deleteTask(id);
            loadTasks();
        }
    };

    const handleEdit = (task) => {
        setEditingTask(task);
        setIsFormVisible(true);
    };

    const handleNew = () => {
        setEditingTask(null);
        setIsFormVisible(true);
    };

    return (
        <div className="container mt-5">
            <h1 className="mb-4">Gerenciador de Tarefas</h1>

            {/* Controls: Filter and New Button */}
            <div className="d-flex justify-content-between mb-3">
                <div className="d-flex align-items-center">
                    <label className="me-2 fw-bold">Filtrar por Status:</label>
                    <select 
                        className="form-select w-auto" 
                        value={filter} 
                        onChange={(e) => setFilter(e.target.value)}
                    >
                        <option value="Todos">Todos</option>
                        <option value="Pendente">Pendente</option>
                        <option value="EmProgresso">Em Progresso</option>
                        <option value="Concluida">Concluída</option>
                    </select>
                </div>
                {!isFormVisible && (
                    <button className="btn btn-success" onClick={handleNew}>
                        + Nova Tarefa
                    </button>
                )}
            </div>

            {/* Form Section */}
            {isFormVisible && (
                <TaskForm 
                    currentTask={editingTask} 
                    onSave={handleSave} 
                    onCancel={() => setIsFormVisible(false)} 
                />
            )}

            {/* Task List Table */}
            <div className="card shadow-sm">
                <div className="card-body">
                    <table className="table table-hover">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Descrição</th>
                                <th>Data Criação</th>
                                <th>Conclusão</th>
                                <th>Status</th>
                                <th>Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            {filteredTasks.map((task) => (
                                <tr key={task.id}>
                                    <td>{task.id}</td>
                                    <td>{task.titulo}</td>
                                    <td>{task.descricao}</td>
                                    <td>{new Date(task.dataCriacao).toLocaleDateString()}</td>
                                    <td>
                                        {task.dataConclusao 
                                            ? new Date(task.dataConclusao).toLocaleDateString() 
                                            : '-'}
                                    </td>
                                    <td>
                                        <span className={`badge ${
                                            task.status === 'Concluida' ? 'bg-success' : 
                                            task.status === 'EmProgresso' ? 'bg-warning text-dark' : 
                                            'bg-secondary'
                                        }`}>
                                            {task.status}
                                        </span>
                                    </td>
                                    <td>
                                        <button 
                                            className="btn btn-sm btn-outline-primary me-2"
                                            onClick={() => handleEdit(task)}
                                        >
                                            Editar
                                        </button>
                                        <button 
                                            className="btn btn-sm btn-outline-danger"
                                            onClick={() => handleDelete(task.id)}
                                        >
                                            Excluir
                                        </button>
                                    </td>
                                </tr>
                            ))}
                            {filteredTasks.length === 0 && (
                                <tr>
                                    <td colSpan="7" className="text-center text-muted">
                                        Nenhuma tarefa encontrada.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}

export default App;