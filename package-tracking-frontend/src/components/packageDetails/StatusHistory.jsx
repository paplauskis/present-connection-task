export default function StatusHistory({ statusHistory }) {
  return (
    <section>
      <h2>Status History</h2>
      {statusHistory && statusHistory.length > 0 ? (
        <ul>
          {statusHistory.map((sh, index) => (
            <li key={index}>
              {sh.status} - {new Date(sh.createdAt).toLocaleString()}
            </li>
          ))}
        </ul>
      ) : (
        <p>No status history available.</p>
      )}
    </section>
  )
}