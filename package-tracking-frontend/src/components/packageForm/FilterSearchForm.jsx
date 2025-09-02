export default function FilterSearchForm({
  statusFilter,
  senderFilter,
  recipientFilter,
  setStatusFilter,
  setSenderFilter,
  setRecipientFilter,
  onSubmit,
  onReset
}) {
  return (
    <form onSubmit={onSubmit} >
      <input
        type="text"
        placeholder="Status"
        value={statusFilter}
        onChange={(e) => setStatusFilter(e.target.value)}
      />
      <input
        type="text"
        placeholder="Sender Name"
        value={senderFilter}
        onChange={(e) => setSenderFilter(e.target.value)}
      />
      <input
        type="text"
        placeholder="Recipient Name"
        value={recipientFilter}
        onChange={(e) => setRecipientFilter(e.target.value)}
      />
      <button type="submit">Search</button>
      <button type="button" onClick={onReset}>
        Reset
      </button>
    </form>
  )
}